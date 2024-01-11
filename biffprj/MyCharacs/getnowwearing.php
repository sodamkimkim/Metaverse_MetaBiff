<?php

$servername = "localhost:3306";
$username = "root";
$password = "asd123";
$dbname = "biff";

$nickName = $_POST["nickName"];
/*
$nickname = $_POST["nickname"];
$Clothes = $_POST["Clothes"];
$Shoes = $_POST["Shoes"];
$Hands = $_POST["Hands"];
$Head = $_POST["Head"];
$Bag = $_POST["Bag"];
$Pet = $_POST["Pet"];
*/

$conn = new mysqli($servername, $username, $password, $dbname);
if($conn->connect_error)
{
	die("connection failed : ". $conn->connect_error);
}
// select * from nowWearingInfo where characterNick = "sodam1";
 $sql = "select * from nowWearingInfo where characterNick = '".$nickName."'";
$result = $conn->query($sql);
if($result->num_rows > 0)
{
	echo "[";
	while($row = $result->fetch_assoc()) // record 하나 $row에 저장
	{
		echo "{'nickname': '".$row['characterNick']."', 'Clothes': '".$row['Clothes']."', 'Hands': '".$row['Hands']."', 'Head': '".$row['Head']."', 'Bag': '".$row['Bag']."', 'Pet': '".$row['Pet']."'},";
	}
	echo "]";
}
else{
	echo "No UserInformation";
}
$conn->close();
?>



