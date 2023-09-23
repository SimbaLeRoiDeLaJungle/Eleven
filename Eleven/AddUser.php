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
$realHash = hash('sha256',$secretKey);
if($realHash == $hash) 
{ 
	$sth = $dbh->prepare('SELECT * FROM users WHERE username =:username OR email = :email');
    $sth->bindParam(':username', $_GET['username'], PDO::PARAM_STR);
    $sth->bindParam(':email', $_GET['email'], PDO::PARAM_STR);
    $result = $sth->fetchAll();
	$count = 0;
	if (count($result) > 0) 
	{
        foreach($result as $r)
        {
            if(strcmp($r['username'],$_GET['username']))
            {
                echo "Username all ready exist";
            }
            else if(strcmp($r['email'],$_GET['email']))
            {
                echo "Email all ready exist";
            }
        }
    }
    else
    {
        try
        {
            echo "1ssssss";
            echo $_GET['password'];
            $sth = $dbh->prepare('INSERT INTO users(`username`,`password`,`email`) VALUES(:username,:pass,:email)');
            echo $_GET['username'];
            $sth->bindParam(':username', $_GET['username'], PDO::PARAM_STR);
            echo $_GET['password'];
            $sth->bindParam(':pass', $_GET['password'], PDO::PARAM_STR);
            echo $_GET['email'];
            $sth->bindParam(':email', $_GET['email'], PDO::PARAM_STR);
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