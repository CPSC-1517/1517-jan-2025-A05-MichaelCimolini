// See https://aka.ms/new-console-template for more information

using OOPsReview;
/*
 * We will be doing our Lesson 9 overview here
 */

Console.WriteLine("Collection Queries");

List<Employment> employments = CreateCollection();


Console.WriteLine("\nFor Loop");
for (int i = 0; i < employments.Count; i++)
{
    Console.WriteLine(employments[i]);
}

Console.WriteLine("\nForEach Loop");
//var = Employment type
//Your compiler will sort this out
foreach (var employment in employments)
{
    Console.WriteLine(employment.ToString());
}

Console.WriteLine("\n--Finding Employment--");

//The employment matching our search
Employment? foundEmployment = null;

//Our search term
const string SEARCHTERM = "PG";

//Finds our item in 2 iterations
for (int i = 0;i < employments.Count;i++)
{
    if (employments[i].Title.Equals(SEARCHTERM))
    {
        foundEmployment = employments[i];
        break;
    }
}

TestIfFound(foundEmployment, SEARCHTERM);
foundEmployment = null;

//Finds our item in 2 iterations
foreach (var employment in employments)
{
    if(employment.Title.Equals(SEARCHTERM))
    {
        foundEmployment = employment;
        break;
    }
}

TestIfFound(foundEmployment, SEARCHTERM);
foundEmployment = null;

//Using inbuilt function Find
//This is identical to our loops above
//e is a replacemente for var employment in our foreach loop
foundEmployment = employments.Find(e => e.Title.Equals(SEARCHTERM));

TestIfFound(foundEmployment, SEARCHTERM);

//Returns the first result
foundEmployment = employments.Find(e => e.Title.Contains(SEARCHTERM));

TestIfFound(foundEmployment, SEARCHTERM);

//Returns the last result
foundEmployment = employments.FindLast(e => e.Title.Contains(SEARCHTERM));

TestIfFound(foundEmployment, SEARCHTERM);


Console.WriteLine("\n-Any and All--");
//Any and All inbuilt methods
SupervisoryLevel searchLevel = SupervisoryLevel.TeamLeader;

if (employments.Any(e => e.Level == searchLevel))
{
    Console.WriteLine($"\nEmployee was once a {searchLevel}");
}
else
{
    Console.WriteLine($"\nEmployee has not been a {searchLevel}");
}

Console.WriteLine("\n--Using LINQ--");

//LINQ generally returns a collection
List<Employment>? foundCollection = null;

//Need to cast our results from Where back to a list with .ToList()
//This uses the LINQ Method style
foundCollection = employments.Where(e => e.Title.Contains(SEARCHTERM)).ToList();

foreach(Employment employment in foundCollection)
{
    Console.WriteLine($"\nAn employment matching {SEARCHTERM} was found: {employment}.");
}

//This uses the LINQ Query stlye
foundCollection = (
    from e in employments
    where e.Title.Contains(SEARCHTERM)
    select e).ToList();

#region Helper Methods

static void TestIfFound(Employment? foundEmployment, string searchTerm)
{
    if (foundEmployment == null)
    {
        Console.WriteLine($"\nPerson had no {searchTerm} employment");
    }
    else
    {
        Console.WriteLine($"\nPerson had {searchTerm} employment. \nEmployment: {foundEmployment}.");
    }
}

/*
 * Creates a starting collection of employment data.
 */
static List<Employment> CreateCollection()
{
    List<Employment> newCollection = new List<Employment>();
    
    newCollection.Add(new Employment("PG I", SupervisoryLevel.Entry,
                        DateTime.Parse("May 1, 2010"), 0.5));
    newCollection.Add(new Employment("PG II", SupervisoryLevel.TeamMember,
                    DateTime.Parse("Nov 1, 2010"), 3.2));
    newCollection.Add(new Employment("PG III", SupervisoryLevel.TeamLeader,
                    DateTime.Parse("Jan 6, 2014"), 8.6));
    newCollection.Add(new Employment("SP I", SupervisoryLevel.Supervisor,
                    DateTime.Parse("Jul 22, 2022"), 2.25));
    
    return newCollection;

    #endregion
}