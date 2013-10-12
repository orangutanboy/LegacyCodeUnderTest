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

        protected virtual string GetSingleResult(string sql)
        {
            return DatabaseWrapper.GetSingleResult(sql);
        }

        protected virtual ArrayList GetManyResults(string sql)
        {
            return DatabaseWrapper.GetManyResults(sql);
        }

        public ArrayList GetData(string code, string gdr, string filterType, bool getAge)
        {
            ArrayList results = null;

            var sql = "select id from people where type = " + _type + " and gender ='" + gdr + "'";
            string values = GetSingleResult(sql);
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
                        results = GetManyResults(sql);
                        break;
                    case "1493":
                        sql = "select dogs.name, dogbreeds.breed from";
                        sql += " dogs inner join dogbreeds on";
                        sql += " dogs.breed = breed.breed where dogs.ownerId in(" + values + ")";
                        if (filterType == "12")
                        {
                            sql += " and coat='curly'";
                        }
                        results = GetManyResults(sql);
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
                        results = GetManyResults(sql);
                        break;
                }
            }
            return results;
        }
    }
}