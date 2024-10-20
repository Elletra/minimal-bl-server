function GameConnection::startLoad(%this)
{
	%this.score = 0;

	secureCommandToAll("zbR4HmJcSY8hdRhr", 'ClientJoin', %this.getPlayerName(), %this, %this.getBLID(), %this.score, false, false, false);
	commandToClient(%this, 'NewPlayerListGui_UpdateWindowTitle', $Pref::Server::Name, $Pref::Server::MaxPlayers);

	%this.loadMission();
}
