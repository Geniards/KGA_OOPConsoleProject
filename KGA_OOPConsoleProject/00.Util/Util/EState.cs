using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGA_OOPConsoleProject.Util
{
    public enum EState
    {
        Alive, Dead, Hit, ESTATE_MAX
    }

    public enum EItemType
    {
        Potion, MapReSearch, Jump, EITEM_TYPE_MAX
    }

    public enum EShortKey
    {
        None = 0, Up, Down, Left, Right, Inven = 6, ESHORTKEY_MAX
    }

    public enum EStage
    {
        None, ESTAGE_MAX = 3
    }
}
