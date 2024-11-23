using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W_QLS_EF.Ctrl_QLSach
{
    //su dung entity framework
    
    internal class Ctrl_LoaiSach
    {
        //QLSach_HDTEntities db = new QLSach_HDTEntities();
        //truy van tat cac cac loai sach
        public List<LoaiSach> findAll()
        {
            return CUtils.db.LoaiSaches.ToList();
        }
        public List<LoaiSach> findByName(string name)
        {
            return CUtils.db.LoaiSaches.ToList().Where(t=>t.TenLoai.Contains(name)).ToList();   
        }
        public void upDate(LoaiSach loaiSach)
        {
            CUtils.db.SaveChanges();
        }
        public void add(LoaiSach loaiSach)
        {
            CUtils.db.LoaiSaches.Add(loaiSach);
            CUtils.db.SaveChanges() ;
        }
        public void remove(LoaiSach loaiSach)
        {
            CUtils.db.LoaiSaches.Remove(loaiSach);
            CUtils.db.SaveChanges() ;
        }
    }
}
