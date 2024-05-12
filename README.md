# KPZKravchenko
Lab 6 for KPZ. Minesweeper

## Description
This is classic realization of minesweeper game.
- You can play in 3 different levels: Easy, Medium and Hard.
- Game contains records list for all levels of difficulty so dont forget to say your name and save the record if you play good enought.


## Levels
### Easy
On this level you have 9x9 field and 10 bombs to find.
### Medium
On this level you have 16x16 field and 40 bombs to find.
### Hard
On this level you have 32x16 field and 99 bombs to find.

Good luck!

## Programming principels
- KISS
- DRY - Repeating parts was moved to methods
- Single Responsibility Principle - Every class make just one thing
- Interface Segregation Principle
- YAGNI - was removed all unused code

## Design patterns
- Template method
- Factory method
- Observer

## Refactoring techniques
- Extract method (GetTimer)
- Inline Temp (IsBomb)
- Remove Assignments to Parameters
- Substitute Algorithm (Few times was changed algorithm for field generation and zero cell click.)
- Encapsulate Field
- Replace Data Value with Object (Changed Xcoord and Ycoord to Coords struct)
- Replace Array with Object
- Replace Type Code with Class
- Replace Nested Conditional with Guard Clauses