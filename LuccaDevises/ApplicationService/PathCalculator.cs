using ApplicationService.Abstractions;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationService
{
    public class PathCalculator : IPathCalculator
    {
        
        private readonly string source;
        private readonly string target;
        public List<Change> pathes = new List<Change>();
        public PathCalculator(List<Change> currencyPathes, string sourceCurrency, string targetCurrency)
        {
            source = sourceCurrency;
            target = targetCurrency;
            pathes.AddRange(currencyPathes);

            // adding inversed change rates 
            foreach (var item in currencyPathes)
            {
                pathes.Add(new Change(item.TargetCurrency, item.SourceCurrency, 1 / item.Rate));
            }
            
        }
        

        public IList<Change> Rates()
        {
            List<List<Change>> foundPathes = FindRatesPathes(source, target, pathes.ToArray());
            return foundPathes.OrderBy(m => m.Count).FirstOrDefault();
        }

        /// <summary>
        /// Recursive search of possible pathes from baseCurrency to targetCurrency
        /// </summary>
        /// <param name="baseCurrency">currency from where the search starts</param>
        /// <param name="targetCurrency">Seeked currency</param>
        /// <param name="remainingPathes">remaining pathes to explore</param>
        /// <returns>list of working pathes</returns>
        private List<List<Change>> FindRatesPathes(string baseCurrency, string targetCurrency, Change[] remainingPathes)
        {
            List<List<Change>> results = new List<List<Change>>();

            List<Change> possible = remainingPathes.Where(r => r.SourceCurrency == baseCurrency).ToList();
            List<Change> hits = possible.Where(p => p.TargetCurrency == targetCurrency).ToList();
            if (hits.Count > 0)
            {
                // possible path toward target found
                possible.RemoveAll(hits.Contains);
                results.AddRange(hits.Select(hit => new List<Change> { hit }));
            }

            Change[] newPathes = remainingPathes.Where(item => !possible.Contains(item)).ToArray();
            foreach (Change possibleRate in possible)
            {
                List<List<Change>> otherConversions = FindRatesPathes(possibleRate.TargetCurrency, targetCurrency, newPathes);
                // insert previous change rate when result found
                otherConversions.ForEach(result => result.Insert(0, possibleRate));
                results.AddRange(otherConversions);
            }
            return results;
        }
    }
}
