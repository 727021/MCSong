using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MCSong
{
    public class ChatBuffer
    {
        public ChatBuffer()
        {
            this.buffer = new List<string>();
            this.currentIndex = 0;
        }

        private List<string> buffer;// [0:"Oldest", 1:"Older", 2:"Newer", 3:"Newest"]
        private int currentIndex;

        public void addEntry(string s)
        {
            buffer.Add(s);
            currentIndex = buffer.Count - 1;
        }

        public string up()
        {
            if (buffer.Count <= 0) { return ""; }
            string temp = "";
            if (currentIndex >= 0)
            {
                temp = buffer.ElementAt(currentIndex);
            }
            if (currentIndex > 0)
            {
                currentIndex--;
            }
            return temp;
        }

        public string down()
        {
            if (currentIndex < (buffer.Count-1))
            {
                currentIndex++;
                return buffer.ElementAt(currentIndex);
            }
            return "";
        }

        public void clear()
        {
            this.buffer.Clear();
            this.currentIndex = 0;
        }
    }
}
