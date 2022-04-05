using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothesForHandsMaterials
{
    class MaterialSupplier
    {
        private int materialID;
        private int supplierlID;
        public void setMaterialID(int materialID)
        {
            this.materialID = materialID;
        }
        public int getMaterialID()
        {
            return materialID;
        }
        public void setSupplierID(int supplierlID)
        {
            this.supplierlID = supplierlID;
        }
        public int getSupplierlID()
        {
            return supplierlID;
        }
    }
}
