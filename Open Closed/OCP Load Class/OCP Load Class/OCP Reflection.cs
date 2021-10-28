using System;

namespace OCP_Load_Class
{
	/// <summary>
	/// An example of OCP to spin up multiple instances of a project
	/// but with each one for exmaple, processing a different messages 
	/// from specific topic from kafka message bus
	/// </summary>
	public class OCP_Reflection
	{
		public static void RunConsumer(string ConsumerName)
		{
			// Specify using a string value the class to run
			var container 
				= Activator.
					CreateInstance(
						"OCP Load Class",					// Assembly reference
						"OCP_Load_Class." + ConsumerName);	// namespace.class

			// Get an actual instance of the class we can use
			ITopicConsumer activeTopicProcessor 
				= (ITopicConsumer) container.Unwrap();
			
			// Run the topic consumer we want
			activeTopicProcessor.RunConsumer();
		}
	}
}
