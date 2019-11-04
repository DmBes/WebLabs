using System.Collections.Generic;
using ASP.DAL.Entities;

namespace ASP.DAL.Entities
{
    public class Boots
    {
        public int BootsId { get; set; } // id boots
        public string BootsName { get; set; } // name boots
        public string Description { get; set; } // Description boots
        
        public int Size { get; set; } // size boots 
        public string Image { get; set; } // name file image
                                          // Навигационные свойства
        // <summary>
        // spicification boots
      // </summary>
        public int BootsGroupId { get; set; }
        public BootsGroup Group { get; set; }
    }
}


public class BootsGroup
{
    public int BootsGroupId { get; set; }
    public string GroupName { get; set; }       
    /// <summary>
    /// Навигационное свойство 1-ко-многим
    /// </summary>
    public List<Boots> Dishes { get; set; }

    public BootsGroup()
    {
        var i = GetResult();
    }
    public int GetResult() => 1;



}


