<?php
$servername = "localhost:3306";
$username = "root";
$password = "asd123";
$dbname = "biff";

$nickName = $_POST["nickName"];
$money = $_POST["money"];
$itemslot1 = $_POST["itemslot1"];
$itemslot2 = $_POST["itemslot2"];
$itemslot3 = $_POST["itemslot3"];
$itemslot4 = $_POST["itemslot4"];
$itemslot5 = $_POST["itemslot5"];
$itemslot6 = $_POST["itemslot6"];
$itemslot7 = $_POST["itemslot7"];
$itemslot8 = $_POST["itemslot8"];
$itemslot9 = $_POST["itemslot9"];
$itemslot10 = $_POST["itemslot10"];
$itemslot11 = $_POST["itemslot11"];
$itemslot12 = $_POST["itemslot12"];
$itemslot13 = $_POST["itemslot13"];
$itemslot14 = $_POST["itemslot14"];
$itemslot15 = $_POST["itemslot15"];
$itemslot16 = $_POST["itemslot16"];
$itemslot17 = $_POST["itemslot17"];
$itemslot18 = $_POST["itemslot18"];
$itemslot19 = $_POST["itemslot19"];
$itemslot20 = $_POST["itemslot20"];

$conn = new mysqli($servername, $username, $password, $dbname);
if($conn->connect_error)
{
	die("connection failed : ". $conn->connect_error);
}

// # update Inventory Info
/*
update inventory set money = 200000, itemslot1 =null, itemslot2 = null, itemslot3 = 'A_umbrella', itemslot4 = 'T_ironman', itemslot5 = 'A_cap', 
itemslot6 = 'A_cap', itemslot7 = 'A_cap', itemslot8 = null, itemslot9 = null, itemslot10 = 'T_ironman', 
itemslot11 = 'T_ironman', itemslot12 = 'A_cap', itemslot13 = 'T_ironman', itemslot14 = 'T_ironman', itemslot15 = 'A_cap', itemslot16 = 'A_cap', 
itemslot17 = 'A_cap', itemslot18 = 'T_ironman', itemslot19 = 'A_cap', itemslot20 = 'T_ironman' where characterNick = "sodam1";
*/
// $sql = "Insert into characterInfo(NickName, userId, model) values('".$nick."', '".$userId."', '".$model."')";
$sql = "update inventory set money = '".$money."', itemslot1 = '".$itemslot1."', itemslot2 = '".$itemslot2."', itemslot3 = '".$itemslot3."', itemslot4 = '".$itemslot4."', itemslot5 = '".$itemslot5."', 
itemslot6 = '".$itemslot6."', itemslot7 = '".$itemslot7."', itemslot8 = '".$itemslot8."', itemslot9 = '".$itemslot9."', itemslot10 = '".$itemslot10."', 
itemslot11 = '".$itemslot11."', itemslot12 = '".$itemslot12."', itemslot13 = '".$itemslot13."', itemslot14 = '".$itemslot14."', itemslot15 = '".$itemslot15."', itemslot16 = '".$itemslot16."', 
itemslot17 = '".$itemslot17."', itemslot18 = '".$itemslot18."', itemslot19 = '".$itemslot19."', itemslot20 = '".$itemslot20."' where characterNick = '".$nickName."'";
$conn->query($sql);
echo $sql;

$conn->close();
?>