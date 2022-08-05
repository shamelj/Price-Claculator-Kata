﻿using PCK.BL.Entities;
using PCK.BL.Repositories;

var book = new Product("The Little Prince", 12345, new(20.25));

// repos
var discountsRepository = new CacheDiscountsRepository();
var absoluteExpensesRepository = new CacheAbsoluteExpensesRepository();
var relativeExpensesRepository = new CacheRelativeExpensesRepository();


// calculaters
var capCalculator = new RelativeCapCalculator(0.2);
var taxCalculator = new TaxCalculator(); TaxCalculator.FlatRateTax = .21;
var discountCalculator = new MultiplicativeDiscountCalculator(discountsRepository);
//var discountCalculator = new AdditiveDiscountCalculator(discountsRepository);

var expensesCalculater = new ExpensesCalculator(absoluteExpensesRepository, relativeExpensesRepository);
AdditiveDiscountCalculator.RelativeDiscountRate = 0.15;
// adding data
discountsRepository.Save(new(0.07, 12345, DiscountType.NonPreceeding));
absoluteExpensesRepository.Save(new("Transport Cost", new(2.2)));
relativeExpensesRepository.Save(new("Packaging Cost", .01));

// Reporter
var reporter = new Reporter();
var netPriceCalculator = new NetPriceCalculator(discountCalculator, taxCalculator, reporter, expensesCalculater,capCalculator);
netPriceCalculator.CalculateNetPrice(book);
