using CombinationOfConcerns;
using NUnit.Framework;
using System.Collections;

namespace CombinationOfConcern.Tests
{
    [TestFixture]
    public class DataTests
    {
        [TestFixture]
        public class GetData
        {
            [Test]
            public void WithEmptyArgs()
            {
                var target = new FakeData("");
                target.GetData("", "", "", false);
                
                const string expected = "select name, population "
                    + "from countries,people on where people.countryname = "
                    + "countries.name and people.id in()";
                
                Assert.That(target.ExecutedSql, Is.EqualTo(expected));
            }

            private class FakeData : Data
            {
                // The sensing variable
                public string ExecutedSql { get; private set; }

                public FakeData(string type)
                    : base(type)
                { }

                protected override string GetSingleResult(string sql)
                {
                    return "";
                }

                protected override ArrayList GetManyResults(string sql)
                {
                    //Store the value in the sensing variable
                    ExecutedSql = sql;
                    return new ArrayList();
                }
            }
        }
    }
}
