<?php
$servername = "localhost:3306";
$username = "root";
$password = "asd123";
$dbname = "biff";

$nickname = $_POST["nickname"];
$Clothes = $_POST["Clothes"];
$Hands = $_POST["Hands"];
$Head = $_POST["Head"];
$Bag = $_POST["Bag"];
$Pet = $_POST["Pet"];

$conn = new mysqli($servername, $username, $password, $dbname);
if($conn->connect_error)
{
	die("connection failed : ". $conn->connect_error);
}

// # update nowWearingInfo Info
/*
update nowWearingInfo set Clothes = 'T_Ironman', Shoes = 'null', Hands = 'null', Head = 'null', 
Bag = 'I_Wing', Pet = 'P_Slime' where characterNick = 'sodam1';
*/
// $sql = "Insert into characterInfo(NickName, userId, model) values('".$nick."', '".$userId."', '".$model."')";
$sql = "update nowWearingInfo set Clothes = '".$Clothes."', Hands = '".$Hands."', Head = '".$Head."', Bag = '".$Bag."', Pet = '".$Pet."'
where characterNick = '".$nickname."'";
$conn->query($sql);
echo $sql;

$conn->close();
?>