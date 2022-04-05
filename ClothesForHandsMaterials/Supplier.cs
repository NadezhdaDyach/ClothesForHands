using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothesForHandsMaterials
{
    class Supplier
    {
        private int ID;
        private String title;
        private String INN;
        private DateTime startDate;
        private int qualityRating;
        private String supplierType;

        public void setID(int ID)
        {
            this.ID = ID;
        }
        public int getID()
        {
            return ID;
        }
        public void setTitle(String title)
        {
            this.title = title;
        }
        public String getTitle()
        {
            return title;
        }
        public void setINN(String INN)
        {
            this.INN = INN;
        }
        public String getINN()
        {
            return INN;
        }
        public void setStartDate(DateTime startDate)
        {
            this.startDate = startDate;
        }
        public DateTime getStartDate()
        {
            return startDate;
        }
        public void setQualityRating(int qualityRating)
        {
            this.qualityRating = qualityRating;
        }
        public int getQualityRating()
        {
            return qualityRating;
        }
        public void setSupplierType(String supplierType)
        {
            this.supplierType = supplierType;
        }
        public String getSupplierType()
        {
            return supplierType;
        }
    }
}
