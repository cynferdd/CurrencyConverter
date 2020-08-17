using DomainService.Abstractions;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DomainService
{
    public class PathCalculator : IPathCalculator
    {
        public PathCalculator()
        {

        }

        /// <summary>
        /// Retrieving the shortest path from a source currency to a target currency, given a set of adjacent edges in a graph
        /// </summary>
        /// <param name="currencyPathes">list of change rates, possible pathes of currency graph</param>
        /// <param name="source">source currency</param>
        /// <param name="target">target currency</param>
        /// <returns>shortest path from source currency to target currency</returns>
        public IList<Change> GetRatesPathes(IList<Change> currencyPathes, string source, string target)
        {
            List<Change> pathes = new List<Change>();
            pathes.AddRange(currencyPathes);

            // adding inversed change rates to get all pathes and better performances
            // since it is inversed, the rate must be modified to 1/rate, rounded to 4 decimals
            foreach (var item in currencyPathes)
            {
                pathes.Add(new Change(item.TargetCurrency, item.SourceCurrency, Math.Round(1 / item.Rate, 4)));
            }

            List<List<Change>> foundPathes = FindRatesPathes(source, target, pathes.ToArray());
            
            // retrieving first path with minimum amount of steps
            return foundPathes.OrderBy(m => m.Count).FirstOrDefault();
        }

        /// <summary>
        /// Recursive search of possible pathes from baseCurrency to targetCurrency
        /// Adapted from Breadth-First Search algorithm
        /// </summary>
        /// <param name="baseCurrency">currency from where the search starts</param>
        /// <param name="targetCurrency">Seeked currency</param>
        /// <param name="remainingPathes">remaining pathes to explore</param>
        /// <returns>list of working pathes</returns>
        private List<List<Change>> FindRatesPathes(string baseCurrency, string targetCurrency, Change[] remainingPathes)
        {
            List<List<Change>> results = new List<List<Change>>();

            List<Change> possible = remainingPathes.Where(r => r.SourceCurrency == baseCurrency).ToList();
            List<Change> target = possible.Where(p => p.TargetCurrency == targetCurrency).ToList();
            if (target.Count > 0)
            {
                // possible path toward target found
                possible.RemoveAll(target.Contains);
                results.AddRange(target.Select(hit => new List<Change> { hit }));
            }

            Change[] newPathes = remainingPathes.Where(item => !possible.Contains(item)).ToArray();
            foreach (Change possibleRate in possible)
            {
                List<List<Change>> foundRatesPathes = FindRatesPathes(possibleRate.TargetCurrency, targetCurrency, newPathes);
                // insert previous change rate when result found
                foundRatesPathes.ForEach(result => result.Insert(0, possibleRate));
                results.AddRange(foundRatesPathes);
            }
            return results;
        }
    }
}
