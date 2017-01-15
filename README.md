# SiS.Lottery
# Quickstart
Restore nuget packages, and run the SiSLottery web project.
Home page contains the forms to create draws, and update draws with results.

# Frameworks/Tools
Autofac - My prefered IoC container.

nUnit/nSubstitute - My preferred unit testing/mocking frameworks.
nUnit Test Adapter - required to run nUnit test within Visual Studio 2015.

# API
Simple Create, Update, Retrive operations implemented via Repository.

POST - /api/Lottery/CreateDraw - Creates a draw with the data below. Checks for Name duplication before adding to repo.
{
    string Name,
    string Description,
    DateTime DrawDate,
    int PrimaryNumberCount,
    int PrimaryNumberLower,
    int PrimaryNumberUpper,
    int SecondaryNumberCount,
    int SecondaryNumberLower,
    int SecondaryNumberUpper
}

PUT - /api/Lottery/UpdateDraw - Updates draw,  with given name, with provided winning Numbers. 
{
    string DrawName,
    IEnumerable<int> PrimaryNumbers,
    IEnumerable<int> SecondaryNumbers
}

GET - /api/Lottery/RetrieveDraws - Retrieves all draws.
GET - /api/Lottery/RetrieveDraws/{date} - Retrieves all draws for give date.

# Interface
Interface was created using JQuery. JQuery is the jaascript framework I am most comfortable using.  Had more time been available, I would have looked into using angular 2.0 to create the UI.

# Tests
nUnit tests have been created for the repository and for the validator.  Code coverage tools such as nCrunch would have made ensuring complete code coverage of the classes in test much easier.

# Time taken
The API and unit tests took around 1 hour 15 mins to complete, whilst the UI took aorund 2 hours to complete.
