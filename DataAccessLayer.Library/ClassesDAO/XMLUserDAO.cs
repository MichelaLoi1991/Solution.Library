using Model.Library;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace DataAccessLayer.Library
{
    public class XMLUserDAO : IUserDAO
    {

        public string Path { get; set; }

        public XMLUserDAO(string path)
        {
            Path = path;
        }
        public List<User> ReadAll()
        {
            XDocument root = XDocument.Load(Path);
            IEnumerable<User> query;
            query = from r in root.Descendants("User")
                    select new User 
                    { 
                        UserId = (int)r.Attribute("UserId"),             Username = (string)r.Attribute("Username"),
                        Password = (string)r.Attribute("Password"),
                        Role = (Roles)Enum.Parse(typeof(Roles),(string)r.Attribute("Role"))
                    };
            return query.ToList();
        }
        public User ReadUserById(int id)
        {
            List<User> allUsers = ReadAll();
            return allUsers.Where(user => user.UserId == id).SingleOrDefault();
        }
        public User GetUserByUserName(string userName)
        {
            List<User> allUsers = ReadAll();
            return allUsers.Where(user => user.Username == userName).SingleOrDefault();
        }

        public void Create(User user)
        {
            XmlDocument dataBase = new XmlDocument();
            dataBase.Load(Path);
            XElement doc = XElement.Load(Path);
            var idList = from r in doc.Descendants("User")
                         select (int)r.Attribute("UserId");
            int lastId = 0;
            if (idList.Any())
                lastId = idList.Max() + 1;
            XmlNode node = dataBase.SelectSingleNode("//User");
            XmlElement root = dataBase.CreateElement("User");
            root.SetAttribute("UserId", lastId.ToString());
            root.SetAttribute("Username", user.Username);
            root.SetAttribute("Password", user.Password);
            root.SetAttribute("Role", user.Role.ToString());
            node.AppendChild(root);
            dataBase.Save(Path);
        }

        public void Update(User user)
        {
            XDocument database = XDocument.Load(Path);
            var query = from u in database.Descendants("User")
                        where (int)u.Attribute("UserId") == user.UserId
                        select u;
            if (!query.Any())
            {
                query.First().SetAttributeValue("Username", user.Username);
                query.First().SetAttributeValue("Password", user.Password);
                query.First().SetAttributeValue("Role", user.Role);
                database.Save(Path);
            }
        }

        public void Delete(User user)
        {
            XDocument database = XDocument.Load(Path);
            var query = from u in database.Descendants("User")
                        where (int)u.Attribute("UserId") == user.UserId
                        select u;
            if (!query.Any())
            {
                query.Remove();
                database.Save(Path);
            }
        }

    }
}
