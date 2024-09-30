function GameConnection::startLoad(%this)
{
	secureCommandToAll("zbR4HmJcSY8hdRhr", 'ClientJoin', %this.getPlayerName(), %this, %this.getBLID(), %this.score, false, false, false);
	secureCommandToClient("zbR4HmJcSY8hdRhr", %this, 'SetMaxPlayersDisplay', $Pref::Server::MaxPlayers);
	secureCommandToClient("zbR4HmJcSY8hdRhr", %this, 'SetServerNameDisplay', $Pref::Player::NetName, $Pref::Server::Name);

	%this.loadMission();
}
