﻿using Models;
using Services;

ClientServiceDB clientServiceDB = new ClientServiceDB();
Client ClientTest = new Client("AB54321", "Katya");
clientServiceDB.AddClient(ClientTest);
var clientGet = clientServiceDB.GetClientByPassID(ClientTest.PassportID);
Console.WriteLine(clientGet.Name);

