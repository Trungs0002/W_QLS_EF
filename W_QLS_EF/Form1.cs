using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using W_QLS_EF.Ctrl_QLSach;
namespace W_QLS_EF
{
    public partial class Form1 : Form
    {
        Ctrl_LoaiSach ctrLoaiSach= new Ctrl_LoaiSach();
        Ctrl_DauSach ctrlDauSach= new Ctrl_DauSach();
        List<LoaiSach> dsLoaiSach = null;
        List<DauSach> dsDauSach = null;
        LoaiSach loaiSach;
        DauSach sach;
        int index;
        public Form1()
        {
            InitializeComponent();
        }
        public void load_LoaiSach()
        {
            var list = from a in dsLoaiSach
                       select new { a.MaLoai, a.TenLoai };
            dtGidViewLoaiSach.DataSource = list.ToList();
           // dtGidViewLoaiSach.Columns[2].Width = 0;
            //dtGidViewLoaiSach.Columns[2].Visible = false;
            //kiem tra ds de hien thi dau sach cho phan tu dau tien
            if (dsLoaiSach.Count > 0)
            {
                loadDauSach(dsLoaiSach[0]);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            dsLoaiSach = ctrLoaiSach.findAll();
            load_LoaiSach();
            if(dsLoaiSach.Count>0)
                loaiSach = dsLoaiSach[0];
        }

        private void dtGidViewLoaiSach_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //}
        }

        private void dtGidViewLoaiSach_RowEnter(object sender, DataGridViewCellEventArgs e)
        {

        }

        void loadDauSach(LoaiSach loaiSach)
        {
            //truy van lay cac thong tin cua dau sach thuoc loai sach 
      
            var list = from a in loaiSach.DauSaches
                       select new { a.Masach, a.TuaSach, a.TacGia,a.NgayXuatBan, a.DonGia, a.SoLuong };
           // dtGridViewSach.DataSource = loaiSach.DauSaches.ToList();
            dtGridViewSach.DataSource = list.ToList();
            
        }
        private void dtGidViewLoaiSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
             DataGridViewRow row = dtGidViewLoaiSach.CurrentRow;
             index = row.Index;
           // int index = e.RowIndex;
            if (index >= 0)
            {
                loaiSach = dsLoaiSach[index];
                txtMaLoai.Text = loaiSach.MaLoai + "";
                txtTenLoai.Text = loaiSach.TenLoai;
               // dtGridViewSach.DataSource = loaiSach.DauSaches.ToList();
               loadDauSach(loaiSach);
                //ham xu ly load du lieu : trich xuat cac truong du lieu tuong ung
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (loaiSach != null)
            {               
                int maloai = int.Parse(txtMaLoai.Text);
                string tenLoai = txtTenLoai.Text;
                //kiem tra ten moi co bi trung voi cac ten loai da co hay ko?
                int counter=dsLoaiSach.Where(t=>t.TenLoai==tenLoai).ToList().Count;
                if (counter > 0)
                {
                    MessageBox.Show("Tên loại bị trùng!");
                    return;
                }
                else
                {
                    loaiSach.TenLoai = tenLoai;                    
                    //goi phuong thuc cap nhat
                    ctrLoaiSach.upDate(loaiSach);
                    //load lai dtgridview
                    dtGidViewLoaiSach.DataSource = null;
                    //dtGidViewLoaiSach.DataSource = dsLoaiSach;
                    load_LoaiSach();
                }
            }
            else
                MessageBox.Show("Chưa chọn loại sách để cập nhật!!!");
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            loaiSach = new LoaiSach();
            loaiSach.MaLoai=int.Parse(txtMaLoai.Text);
            loaiSach.TenLoai=txtTenLoai.Text.Trim();
            dsLoaiSach.Add(loaiSach);
            //goi controler
            ctrLoaiSach.add(loaiSach);
            //load lai du lieu
            dtGidViewLoaiSach.DataSource=null;
            load_LoaiSach();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (loaiSach != null)
            {
                if(loaiSach.DauSaches.Count > 0)
                {
                    MessageBox.Show("Không thể xoá loại sách đã có đầu sách");
                    return;
                }
                dsLoaiSach.Remove(loaiSach);
                ctrLoaiSach.remove(loaiSach);
                dtGidViewLoaiSach.DataSource = null;
                load_LoaiSach();
                loaiSach = null;
            }
        }

        private void btnTiep_Click(object sender, EventArgs e)
        {
            txtMaLoai.Text = null;
            txtTenLoai.Text=null;
            txtMaLoai.Focus();
        }

        private void dtGridViewSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dtGridViewSach.CurrentRow;
            int idrow = row.Index;
            if(idrow>=0)
            {
                sach = loaiSach.DauSaches.ToList()[idrow];
                txtMaSach.Text = sach.Masach + "";
                txtTuaSach.Text=sach.TuaSach;
                txtTacGia.Text = sach.TacGia;
                txtDonGia.Text = sach.DonGia + "";
                txtSoLuong.Text=sach.SoLuong+"";
                dtimeNgayXB.Text = sach.NgayXuatBan + "";
            }
        }

        private void btnSuaSach_Click(object sender, EventArgs e)
        {
            if(sach!=null)
            {
                sach.TuaSach=txtTuaSach.Text;
                sach.TacGia=txtTacGia.Text;
                sach.NgayXuatBan = dtimeNgayXB.Value;
                sach.DonGia=int.Parse(txtDonGia.Text);
                sach.SoLuong=int.Parse(txtSoLuong.Text);
                //cập nhật ở model
                ctrlDauSach.upDate(sach);
                //cập nhật ở view: datagridview
                dtGridViewSach.DataSource = null;
                loadDauSach(loaiSach);
            }
        }

        private void btnThemSach_Click(object sender, EventArgs e)
        {
           
            int maSach=int.Parse(txtMaSach.Text);
            string tuaSach = txtTuaSach.Text.Trim();
            string tacGia=txtTacGia.Text.Trim();
            DateTime dt = dtimeNgayXB.Value;
            int soLuong = int.Parse(txtSoLuong.Text);
            decimal donGia=decimal.Parse(txtDonGia.Text);
            //kiem tra tinh hop le cua du lieu Sach
            sach = new DauSach();
            sach.Masach=maSach;
            sach.TuaSach=tuaSach;
            sach.TacGia =tacGia;
            sach.NgayXuatBan = dt;
            sach.SoLuong=soLuong;
            sach.DonGia = donGia;
            sach.LoaiSach=loaiSach;
            ctrlDauSach.add(sach);
            //cập nhật ở view: datagridview
            dtGridViewSach.DataSource = null;
            loadDauSach(loaiSach);
        }

        private void btnXoaSach_Click(object sender, EventArgs e)
        {
            if(sach!= null)
            {
              //kiem tra sach da duoc ban ra chua?
              if(sach.CT_HOADON.Count>0)
                {
                    MessageBox.Show("Không thể xoá!");
                    return;
                }
                ctrlDauSach.remove(sach);
                dtGridViewSach.DataSource = null;
                loadDauSach(loaiSach);
                sach = null;
            }
        }
    }
}
