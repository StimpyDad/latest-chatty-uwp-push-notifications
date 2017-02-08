﻿using System.Collections.Generic;
using System.Threading.Tasks;
using SNPN.Model;

namespace SNPN.Data
{
	public interface IUserRepo
	{
		Task AddOrUpdateDevice(NotificationUser user, DeviceInfo notificationInfo);
		Task<NotificationUser> AddUser(NotificationUser user);
		Task DeleteDevice(string deviceId);
		Task DeleteDeviceByUri(string uri);
		Task<NotificationUser> FindUser(string userName);
		Task<List<string>> GetAllUserNamesForNotification();
		Task<IEnumerable<DeviceInfo>> GetUserDeviceInfos(NotificationUser user);
		Task UpdateUser(NotificationUser user);
	}
}