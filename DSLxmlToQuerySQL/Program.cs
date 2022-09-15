using System.Xml;




bool result = true;
var arrayTowns = new List<string>();
arrayTowns.Add("id");
arrayTowns.Add("name");
arrayTowns.Add("population");
arrayTowns.Add("code");
arrayTowns.Add("average_age");
arrayTowns.Add("district_code");
arrayTowns.Add("departement_code");
arrayTowns.Add("region_code");
arrayTowns.Add("region_name");


var arrayDSL = new List<string>();
var arrayFilter = new List<string>();

XmlDocument doc = new XmlDocument();

string xmlData = "<query><fields><field>name</field><field>population</field><field>code</field></fields><filters><field>name</field><value>200</value></filters></query>";
doc.LoadXml(xmlData);

foreach (XmlNode node in doc.DocumentElement.ChildNodes)
{
    Console.WriteLine(node.Name + " ");
    if (node.Name == "fields")
    {
        foreach (XmlNode fieldsNode in node.ChildNodes)
        {
            string text = fieldsNode.InnerText;
            if (arrayTowns.Contains(text))
            {
                arrayDSL.Add(text);
            }
            else
            {
                result = false;
                break;
            }

            if (!result) break;
        }
    }

    if (node.Name == "filters")
    {
        foreach(XmlNode filtersNode in node.ChildNodes)
        {
            if (filtersNode.Name != "predicate")
            {
                arrayFilter.Add(filtersNode.InnerText);
            }else
            {
                switch (filtersNode.InnerText)
                {
                    case "eq":
                        arrayFilter.Add("=");
                        break;
                    case "gt":
                        arrayFilter.Add(">");
                        break;
                    case "it":
                        arrayFilter.Add("<");
                        break;
                    case "contains":
                        arrayFilter.Add("CONTAINS");
                        break;
                    default:
                        arrayFilter.Add("=");
                        break;
                }
            }
        }
    }
    
    
    
}

if (arrayFilter.Count != 3)
{
    arrayFilter.Add("=");
}

if (result)
{
    Console.WriteLine("SELECT " + String.Join(",", arrayDSL) + " FROM towns" + " WHERE " + arrayFilter[0] + " " + arrayFilter[2] + " " + arrayFilter[1]);
}else
{
    Console.WriteLine("Error Fields");
}


