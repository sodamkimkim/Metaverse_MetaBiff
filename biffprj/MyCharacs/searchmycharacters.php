<?php

$servername = "localhost:3306";
$username = "root";
$password = "asd123";
$dbname = "biff";

$id = $_POST["id"];

$conn = new mysqli($servername, $username, $password, $dbname);
if($conn->connect_error)
{
	die("connection failed : ". $conn->connect_error);
}
// select * from characterInfo where  userId = "theka265";
// Insert into characterInfo(NickName, userId, model) values("sodam2", "theka265", "F_KimHyeSoo"); 
$sql = "select * from characterInfo where userId = '".$id."'";
$result = $conn->query($sql);
if($result->num_rows > 0)
{
	echo "[";
	while($row = $result->fetch_assoc()) // record 하나 $row에 저장
	{
		echo "{'nickName': '".$row['NickName']."', 'userId': '".$row['userId']."', 'model': '".$row['model']."'},";
	}
	echo "]";
}

else{
	echo "No UserInformation";
}
$conn->close();
?>