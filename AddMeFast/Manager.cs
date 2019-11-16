using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeFast
{
    public  class Manager
    {

        public List<string> GetList(string path)
        {
           return  File.ReadLines(path).ToList();
        }

    }
}
