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
$realHash = hash('sha256',  $secretKey);

if($realHash == $hash) 
{ 
	$sth = $dbh->query('SELECT * FROM users');
	$result = $sth->fetchAll();
	$name = $_GET['username'];
    $pass = $_GET['password'];
    $find = false;
    if (count($result) > 0) 
	{
		foreach($result as $r) 
		{
			if(strcmp($r['username'],$name))
			{
                
                if(strcmp($r['password'],$pass))
                {
                    echo "Connection";
                }
                else
                {
                    echo "Bad password";
                }
                $find = true;
                break;
			}	
		}
	}    
}
if(!$find)
{
    echo "Not-Find";
}


?>