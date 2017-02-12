﻿using System.Xml.Linq;

namespace SNPN.Common
{
	public class QueuedNotificationItem
	{
		public QueuedNotificationItem(NotificationType type, XDocument content, string uri = null, NotificationGroups group = NotificationGroups.None, string tag = null, int ttl = 0)
		{
			this.Type = type;
			this.Content = content;
			this.Uri = uri;
			this.Group = group;
			this.Tag = tag;
			this.Ttl = ttl;
		}

		public XDocument Content { get; private set; }
		public NotificationType Type { get; private set; }
		public string Uri { get; private set; }
		public NotificationGroups Group { get; private set; }
		public string Tag { get; private set; }
		public int Ttl { get; private set; }
	}
}
