# PromotionEngine

Promotion Engine Problem Statement: A simple promotion engine for a checkout process. Our Cart contains a list of single character SKU ids (A, B, C. ) over which the promotion engine will need to run.
The promotion engine will calculate the total order value.

## Project State

Completed

## Technologies Used

* .Net Core 3.1.6  
* XUnit for Unit testing the project.  
* Microsoft.Extensions.Hosting for Dependency Injection  

## Project Type

.Net Core Console Application is used in order to create PromotionEngine.  
It can also made into Rest Api by injected dependencies of BusinessLogic.
It is assumed that  list of Items in the Cart are entered in the form of A,B,C,D items only and rest of the items will be ignored during calculation of the order value.  

## Project Structure

### Models

Contains Domain related classes which will be used in the main logic.

### BusinessLogic

Contains the main logic for calculating the total order values in the Cart.  

### Program

Main class of the program where Services will be configured used for dependency injection.  

### Startup

It will boot up the console application and call the main logic for doing the calculation.  

### PromotionEngineTest

It contains the Unit test project for testing the PromotionEngine project.  

## Additional Information

* Promotion engine is able to handle for more promotion types (e.g. a future promotion could be x% of a SKU unit price). Unit test is added for the same.  

* It is assumed that Unit Price will be static for the whole process but it can be extended to add other values.  
