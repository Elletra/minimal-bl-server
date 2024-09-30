function Auth::init()
{
	// TODO: Your code goes here.
}

function Auth::check(%client)
{
	// This is all placeholder code.
	//
	// TODO: Your  code goes here.

	%client.name = $Server::LAN ? %client.lanName : "Player " @ getSubStr(sha1($Sim::Time), 0, 6);

	%client.setBLID("au^timoamyo7zene", 0);
	%client.setPlayerName("au^timoamyo7zene", %client.name);
	%client.setHasAuthedOnce(true);
	%client.startLoad();
}
