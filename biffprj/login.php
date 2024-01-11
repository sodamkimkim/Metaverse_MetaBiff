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
// select userId as id, userPassword as pw from userInfo where userId = "theka265" && userPassword = "asd123";
$sql = "select userId as id, userPassword as pw from userInfo where userId = '".$id."' && userPassword = '".$pw."'";

$result = $conn->query($sql);
if($result->num_rows > 0)
{
	echo "[";
	while($row = $result->fetch_assoc()) // record �ϳ� $row�� ����
	{
		echo "{'id': '".$row['id']. "', 'pw': '".$row['pw']."'},'";
	}
	echo "]";
}
else{
	echo "No UserInformation";
}
$conn->close();
?>