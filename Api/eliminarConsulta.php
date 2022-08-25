<?php 
$servername = "localhost";
$username = "root";
$password = "";
$dbname = "BD_API";
$nombre=$_GET['nom'];
$celular=$_GET['cel'];

try {
  $conn = new PDO("mysql:host=$servername;dbname=$dbname", $username, $password);
  $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
  $stmt = $conn->prepare("DELETE FROM persona WHERE id=".$id);
  $stmt->execute();
  echo "Eliminado";
  
  //eliminarConsulta.php?id=1
  
} catch(PDOException $e) {
  echo "Error: " . $e->getMessage();
}
$conn = null;
echo "</table>";
?>