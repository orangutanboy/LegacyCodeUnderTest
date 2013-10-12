using System.Collections;

namespace CombinationOfConcerns
{
    public class Data
    {
        private readonly string _type;

        public Data(string type)
        {
            _type = type;
        }

        public ArrayList GetData(string code, string gdr, string filterType, bool getAge)
        {
            ArrayList results = null;

            var sql = "select id from people where type = " + _type + " and gender ='" + gdr + "'";
            string values = DatabaseWrapper.GetSingleResult(sql);
            for (int i = 0; i < values.Split(',').Length; i++)
            {
                switch (code)
                {
                    case "128855":
                        sql = "select name";
                        if (getAge)
                            sql += ",age";                    
                        sql += ",gender from people where type in(" + values + ")";
                        if (filterType == "45")
                        {
                            sql += " and gender='" + gdr + "'";
                        }
                        if (filterType == "87")
                        {
                            sql += " and shoesize = 43";
                        }
                        results = DatabaseWrapper.GetManyResults(sql);
                        break;
                    case "1493":
                        sql = "select dogs.name, dogbreeds.breed from";
                        sql += " dogs inner join dogbreeds on";
                        sql += " dogs.breed = breed.breed where dogs.ownerId in(" + values + ")";
                        if (filterType == "12")
                        {
                            sql += " and coat='curly'";
                        }
                        results = DatabaseWrapper.GetManyResults(sql);
                        break;
                    default:
                        sql = "select name, population from countries,people on where people.countryname = countries.name and people.id in(" + values + ")";
                        if (filterType == "f")
                        {
                            sql += " and countries.continent='South America'";
                        }
                        if (filterType == "642")
                        {
                            sql += " and countries.continent='Europe'";
                        }
                        results = DatabaseWrapper.GetManyResults(sql);
                        break;
                }
            }
            return results;
        }
    }
}