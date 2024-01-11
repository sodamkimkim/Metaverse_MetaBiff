<?php
$servername = "localhost:3306";
$username = "root";
$password = "asd123";
$dbname = "biff";

$userId = $_POST["userId"];
$nick = $_POST["nickname"];
$model = $_POST["model"];

$conn = new mysqli($servername, $username, $password, $dbname);
if($conn->connect_error)
{
	die("connection failed : ". $conn->connect_error);
}

// # new character 持失
//Insert into characterInfo(NickName, userId, model) values("sodam2", "theka265", "F_KimHyeSoo"); 
$sql = "Insert into characterInfo(NickName, userId, model) values('".$nick."', '".$userId."', '".$model."')";
$conn->query($sql);
echo $sql;

// # nowWearingInfo 持失
// insert into nowWearingInfo(characterNick) values("sodam1");
// insert into nowWearingInfo values("sodam1", "null","null","null","null","null");
$newNowWearingSql = "insert into nowWearingInfo values('".$nick."',  'null', 'null', 'null', 'null', 'null')";
$conn->query($newNowWearingSql);
echo $newNowWearingSql;

// # inventory 持失
/*
insert into inventory values("theka265","sodam1",  100000, 'null', 'null', 'null', 'null', 'null', 'null', 'null', 'null', 'null', 'null', 'null', 
'null', 'null', 'null', 'null', 'null', 'null', 'null', 'null', 'null');
*/
$newInvenSql = "insert into inventory values('".$userId."', '".$nick."',  300000 , 'null', 'null', 'null', 'null', 'null', 'null', 'null', 'null', 'null', 'null', 'null', 
'null', 'null', 'null', 'null', 'null', 'null', 'null', 'null', 'null')";
$conn->query($newInvenSql);
echo $newInvenSql;

$conn->close();
?>