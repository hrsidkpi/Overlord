using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Overlord.Pages
{
	public interface ITabbedPage<T>
	{

		public void Select(ITabbedPageTab<T> page);

	}
}
