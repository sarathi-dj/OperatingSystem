### 📌 1. Project Title and Description
 **Dj.OS**
A simple custom command-line operating system built using [Cosmos OS](https://github.com/CosmosOS/Cosmos), created by Partha Sarathi.

Dj.OS supports basic shell commands, a simulated file system, a simple calculator, and an interactive learning game to understand core OS concepts like memory management, processes, and interrupts.


### 🚀 2. Features


 **✨ Features**

- 🖥️ Basic Shell Interface with built-in commands:
  - `help`, `clear`, `shutdown`, `about`, `echo`
- 📂 Simulated In-Memory File System:
  - `create`, `write`, `read`, `delete`, `ls`
- 🧮 Simple Calculator:
  - `calc <num1> <operator> <num2>`
- 🎮 OS Learning Game:
  - Learn OS fundamentals: processes, memory, interrupts, and more


### 🛠️ 3. Technologies Used

- 
  **🛠️ Built With**

**🧰 Dependencies**

| Tool                        | Purpose                                  |
|-----------------------------|------------------------------------------|
| Cosmos OS Toolkit           | Build custom C# operating systems        |
| Visual Studio               | IDE for C# development                   |
| .NET SDK                    | Required by Cosmos                       |
| VMware Workstation / Player | Virtual machine to run Dj.OS             |



### 🧾 4. How to Build & Run


 **🧾 Getting Started**

> 🚨 **Requirements:**
>
> - [Visual Studio 2022+](https://visualstudio.microsoft.com/)
> - [.NET SDK]
> - [Cosmos OS User Kit](https://github.com/CosmosOS/Cosmos)
> - **VMware Workstation / Player** – Required to boot and run the OS in a virtual machine

# Clone the Repository

```bash
git clone https://github.com/yourusername/djos.git
cd djos



```

### 🔧 **5. Commands List**

```markdown
 💻 Dj.OS Command List

| Command              | Description                                 |
|----------------------|---------------------------------------------|
| `help`               | Show available commands                     |
| `clear`              | Clear the screen                            |
| `about`              | Info about Dj.OS                            |
| `echo <text>`        | Print back your input text                  |
| `shutdown`           | Shutdown the system                         |
| `create <file>`      | Create a new file in memory                 |
| `write <file> <txt>` | Write text to a file                        |
| `read <file>`        | Read file contents                          |
| `delete <file>`      | Delete a file                               |
| `ls`                 | List all created files                      |
| `calc <n1> <op> <n2>`| Perform arithmetic operation (+ - * /)      |
| `osgame`             | Start the interactive OS Learning Game      |
```

### 🎓 6. OSGame: Learning Mode Commands


 🎮 OSGame Commands

| Command                     | Description                                                      |
|-----------------------------|------------------------------------------------------------------|
| `create process <name>`     | Start a new simulated process                                    |
| `kill process <name>`       | Kill a process and free its memory                               |
| `use memory <MB>`           | Allocate memory to the latest process                            |
| `free memory <MB>`          | Free memory from the latest process                              |
| `interrupt`                 | Simulate a CPU interrupt                                         |
| `status`                    | Show current system memory and process state                     |
| `explain <topic>`           | Learn OS concepts: `process`, `memory`, `scheduler`, `interrupt` |
| `exit`                      | Exit the learning game                                           |


### 👨‍💻 8. Author and Credits


 👨‍💻 Author - **Partha Sarathi**

- [GitHub](https://github.com/sarathi-dj)
- Built using the [Cosmos Project](https://github.com/CosmosOS/Cosmos)
