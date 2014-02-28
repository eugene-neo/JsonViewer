namespace Newtonsoft.Json.Tests.classes
{
    using System.Collections.Generic;

    public class Article
    {
        public string Name;

        public Article()
        {
        }

        public Article(string name)
        {
            Name = name;
        }
    }

    public class ArticleCollection : List<Article>
    {
    }
}