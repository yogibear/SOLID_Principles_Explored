using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OCP_Load_Class
{
	/// <summary>
	/// 
	/// A method of applying OCP principle to list of user choices on a menu for example
	/// </summary>
	public class OCP
	{
		List<ITopicConsumer>
			ConsumerList
				= new List<ITopicConsumer>();

		public OCP(string ConsumerName)
		{
			/*
			 * This would be reasnoble if we wanted to populate a dropdown list
			 * Not so appropriate if you want to spin a cintainer up to run one thing
			 * ... see the reflection based one for that
			 */
			ConsumerList.Add(new TeamsConsumer());
			ConsumerList.Add(new ProductsConsumer());
			ConsumerList.Add(new UsersConsumer());

			ITopicConsumer
				activeConsumer 
					= ConsumerList.
						First(x => x.GetName() == ConsumerName);

			activeConsumer.RunConsumer();
		}
	}
}
