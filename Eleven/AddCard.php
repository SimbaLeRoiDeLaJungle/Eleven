<?php 
$hostname = 'localhost';
$username = 'Eleven';
$password = 'YoloLeGrand';
$database = 'eleven';
$secretKey = 'Yoooolo';

try 
{
	$dbh = new PDO('mysql:host='. $hostname .';dbname='. $database, $username, $password);
} 
catch(PDOException $e) 
{
	echo '<h1>An error has ocurred.</h1><pre>', $e->getMessage() 
            ,'</pre>';
}

$hash = $_GET['hash'];
$realHash = hash('sha256', $_GET['card_id'] . $secretKey);
if($realHash == $hash) 
{ 
	$sth = $dbh->query('SELECT * FROM cards WHERE user_id = 0');
	$result = $sth->fetchAll();
	$pass = false;
	$card_id = $_GET['card_id'];
	$count = 0;
	if (count($result) > 0) 
	{
		foreach($result as $r) 
		{
			if($r['card_id'] == $card_id)
			{
				$pass = true;
				$count = $r['count'];
				break;
			}	
		}
	}
	if(!$pass)
	{
		$sth = $dbh->prepare('INSERT INTO cards VALUES (0,1, :cardid, 0)');
		try 
		{
			$sth->bindParam(':cardid', $card_id, 
					  PDO::PARAM_STR);
			$sth->execute();
		}
		catch(Exception $e) 
		{
			echo '<h1>An error has ocurred.</h1><pre>', 
					 $e->getMessage() ,'</pre>';
		}
	}
	else
	{
		$sth = $dbh->prepare('UPDATE cards SET count = :count WHERE user_id = 0');
		try 
		{
			$count = $count + 1;
			$sth->bindParam(':count', $count, 
					  PDO::PARAM_STR);
			$sth->execute();
		}
		catch(Exception $e) 
		{
			echo '<h1>An error has ocurred.</h1><pre>', 
					 $e->getMessage() ,'</pre>';
		}
	}


}

?>