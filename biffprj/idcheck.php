<?php

$servername = "localhost:3306";
$username = "root";
$password = "asd123";
$dbname = "biff";

$id = $_POST["id"];
$pw = $_POST["pw"];

$conn = new mysqli($servername, $username, $password, $dbname);
if($conn->connect_error)
{
	die("connection failed : ". $conn->connect_error);
}
// select userId as id from userInfo where userId = "theka265";
$sql = "select userId as id from userInfo where userId = '".$id."'";
$result = $conn->query($sql);
if($result->num_rows > 0)
{
	echo "[";
	while($row = $result->fetch_assoc()) // record 하나 $row에 저장
	{
		echo "{'id': '".$row['id']."'},'";
	}
	echo "]";
}

else{
	echo "No UserInformation";
}
$conn->close();
?>