<?php

$servername = "localhost:3306";
$username = "root";
$password = "asd123";
$dbname = "biff";

$nickName = $_POST["nickName"];

$conn = new mysqli($servername, $username, $password, $dbname);
if($conn->connect_error)
{
	die("connection failed : ". $conn->connect_error);
}
// select * from inventory where characterNick = "sodam1"
 $sql = " select * from inventory where characterNick = '".$nickName."'";
$result = $conn->query($sql);
if($result->num_rows > 0)
{
	echo "[";
	/*
	echo "{'nickname': '".$row['characterNick']."', 'model_Clothes': '".$row['model_Clothes']."', 'texture_Clothes': '".$row['texture_Clothes'].
        "', 'model_Shoes': '".$row['model_Shoes']."', 'texture_Shoes': '".$row['texture_Shoes']."', 'model_Hands': '".$row['model_Hands']."', 'texture_Hands': '".$row['texture_Hands']."', 
        'model_Head': '".$row['model_Head']."', 'texture_Head': '".$row['texture_Head']."', 'model_Bag': '".$row['model_Bag']."', 'texture_Bag': '".$row['texture_Bag']."'},";
		*/
	while($row = $result->fetch_assoc()) // record 하나 $row에 저장
	{
		echo "{'userId': '".$row['userId']."', 'characterNick': '".$row['characterNick']."', 'money': '".$row['money'].
        "', 'itemslot1': '".$row['itemslot1']."', 'itemslot2': '".$row['itemslot2']."', 'itemslot3': '".$row['itemslot3']."', 'itemslot4': '".$row['itemslot4']."', 
        'itemslot5': '".$row['itemslot5']."', 'itemslot6': '".$row['itemslot6']."', 'itemslot7': '".$row['itemslot7']."', 'itemslot8': '".$row['itemslot8']."',
		'itemslot9': '".$row['itemslot9']."', 'itemslot10': '".$row['itemslot10']."', 'itemslot11': '".$row['itemslot11']."', 'itemslot12': '".$row['itemslot12']."',
		'itemslot13': '".$row['itemslot13']."', 'itemslot14': '".$row['itemslot14']."', 'itemslot15': '".$row['itemslot15']."', 'itemslot16': '".$row['itemslot16']."',
		'itemslot17': '".$row['itemslot17']."', 'itemslot18': '".$row['itemslot18']."', 'itemslot19': '".$row['itemslot19']."', 'itemslot20': '".$row['itemslot20']."'
		},";
	}
	echo "]";
}
else{
	echo "No CharacterInventoryInformation";
}
$conn->close();
?>



