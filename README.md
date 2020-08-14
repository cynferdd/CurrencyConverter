# CurrencyConverter
Currency Converter based on an incomplete table.

## How to use it
launch the exe with a file path as a parameter.
For example :

``` LuccaDevises ./data.txt```

## File format
First line contains 3 fields separated with a ";" :
 * source currency on 3 characters
 * source amount as a positive integer
 * target currency on 3 characters

Second line is a positive integer showing the number of lines coming next

Remaining lines are Change Rates data represented as follow : 
 * 3 fields separated with a ";"
 * Source currency on 3 characters
 * target currency on 3 characters
 * change rate as a decimal number, with a "." as separator and 4 decimals.

 For example : 
 ```
EUR;550;JPY
6
AUD;CHF;0.9661
JPY;KRW;13.1151
EUR;CHF;1.2053
AUD;JPY;86.0305
EUR;USD;1.2989
JPY;INR;0.6571
```

Output is :

```> 59033```

## Example Command lines and Data files

Some .bat files and associated data files can be found in the ExampleDataFiles directory, showing failing cases and a working case.