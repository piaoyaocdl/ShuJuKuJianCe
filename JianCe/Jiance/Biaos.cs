using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jiance
{
    public class Biaos
    {
        public string biaoming { set; get; }
        public int yuantiaoshu { set; get; }
        public int xintiaoshu
        {
            set
            {
                yuantiaoshu = xintiaoshu;
                _xintiaoshu = value;
            }
            get
            {
                return _xintiaoshu;
            }
        }
        public int bianhua
        {
            get
            {
                return xintiaoshu - yuantiaoshu;
            }
            set {; }
        }

        private int _xintiaoshu;
    }
}
