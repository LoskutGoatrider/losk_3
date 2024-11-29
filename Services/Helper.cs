using losk_3.BasaSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace losk_3.Services
{
        internal class Helper
        {
                private static TELECOMEntities _context;
                public static TELECOMEntities GetContext()
                {
                        if (_context == null)

                        {
                                _context = new TELECOMEntities();
                        }
                        return _context;
                }
                         
        }
        
}
