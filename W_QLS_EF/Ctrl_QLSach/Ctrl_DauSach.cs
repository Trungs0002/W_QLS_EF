using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W_QLS_EF.Ctrl_QLSach
{
    internal class Ctrl_DauSach
    {
        public void upDate(DauSach dausach)
        {
            CUtils.db.SaveChangesAsync();
        }
        public void add(DauSach sach)
        {
            CUtils.db.DauSaches.Add(sach);
            CUtils.db.SaveChangesAsync();
        }
        public void remove(DauSach sach)
        {
            CUtils.db.DauSaches.Remove(sach);
            CUtils.db.SaveChangesAsync();
        }
    }
}
