using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothesForHandsMaterials
{
    class Material
    {
        private int id;
        private String title;
        private int countInPack;
        private String unit;
        private float countInStock;
        private float minCount;
        private String description;
        private float cost;
        private String image;
        private int materialTypeID;
      
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
        public void setCountInPack(int countInPack)
        {
            this.countInPack = countInPack;
        }
        public int getCountInPack()
        {
            return countInPack;
        }
        public void setUnit(String unit)
        {
            this.unit = unit;
        }
        public String getUnit()
        {
            return unit;
        }
        public void setCountInStock(float countInStock)
        {
            this.countInStock = countInStock;
        }
        public float getCountInStock()
        {
            return countInStock;
        }
        public void setMinCount(float minCount)
        {
            this.minCount = minCount;
        }
        public float getMinCount()
        {
            return minCount;
        }
        public void setDescription(String description)
        {
            this.description = description;
        }
        public String getDescription()
        {
            return description;
        }
        public void setCost(float cost)
        {
            this.cost = cost;
        }
        public float getCost()
        {
            return cost;
        }
        public void setImage(String image)
        {
            this.image = image;
        }
        public String getImage()
        {
            return image;
        }
        public void setMaterialTypeID(int materialTypeID)
        {
            this.materialTypeID = materialTypeID;
        }
        public int getMaterialTypeID()
        {
            return materialTypeID;
        }
    }
}
