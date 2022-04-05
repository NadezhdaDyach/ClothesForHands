using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothesForHandsMaterials
{
    class MaterialType
    {
        private int id;
        private String title;

        public void setID(int id)
        {
            this.id = id;
        }
        public int getID()
        {
            return id;
        }
        public void setTitle(String title)
        {
            this.title = title;
        }
        public String getTitle()
        {
            return title;
        }

    }
}
