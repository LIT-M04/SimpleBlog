using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBlog.Data
{
    public class Post
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
    }
}
