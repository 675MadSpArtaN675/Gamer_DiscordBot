using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleBithdayCongratulator.Interfaces
{
    public interface IBithdayDataReader
    {
        string[] ReadDescriptions(int limit);
        string? ReadDescription(int index);
        string ReadRandomDescription();

        string[] ReadTitles(int limit);
        string? ReadTitle(int index);
        string ReadRandomTitle();
    }
}
