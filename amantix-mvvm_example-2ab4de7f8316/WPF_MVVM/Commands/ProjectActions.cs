using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_MVVM.Models;

namespace WPF_MVVM.Commands
{
    public class ProjectActions
    {

        public void Shift(List<Note> ofList, List<Note> inList, Note item) //перемещение Note из одной группы в другую
        {
            try
            {
                inList.Add(item);
                ofList.Remove(item);
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
        }

        public void Remove(List<Note> list, Note item)
        {
            list.Remove(item);
        }
    }
}
