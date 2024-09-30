// Load default preferences.
exec("./defaults.cs");

// Load user preferences to override default preferences.
if (isFile("config/prefs.cs"))
{
	exec("config/prefs.cs");
}

$Server::Type::Internet = 0;
$Server::Type::LAN = 1;
