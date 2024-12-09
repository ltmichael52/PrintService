// Variable to track loading state
var isloading = false;

// Function to be executed every 3 minutes
function taskToRun() {
    if (!isloading) {
        isloading = true; // Set loading state to true
        $.ajax({
            url: '/Printer/ChangePrintingStatus', // Replace with your actual endpoint
            type: 'PUT',
            success: function (response) {
                console.log("Successfully updated printing status:", response);
                isloading = false; // Reset loading state
            },
            error: function (xhr, status, error) {
                console.error("Error changing printing status:", error);
                alert("Failed to change printing status.");
                isloading = false; // Reset loading state
            }
        });
    } else {
        console.log("Task is already in progress, skipping...");
    }
}

// Run the function every 1 minutes
setInterval(taskToRun, 60000);
