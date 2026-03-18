<script setup></script>

<template>
  <header>
    <nav class="navbar navbar-expand-sm navbar-dark border-bottom bg-dark mb-0">
      <div class="container-fluid">
        
        <RouterLink to="/" class="navbar-brand d-flex align-items-center gap-2">
          <img 
          src="../assets/logo.png" 
          alt="Logo" 
          height="40"
          class="d-inline-block align-text-top"
          >
          <span class="fs-4 fw-medium font-primary text-white">ToDoApp</span>
        </RouterLink>

        <button type="button" class="navbar-toggler" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent">
          <span class="navbar-toggler-icon "></span>
        </button>

        <div class="collapse navbar-collapse" id="navbarSupportedContent">
          <ul class="navbar-nav text-center ms-auto">
            <li class="nav-item">
              <RouterLink to="/" class="nav-link text-white font-secundary">Home</RouterLink>
            </li>
            <li class="nav-item">
              <RouterLink to="/tasks" class="nav-link text-white font-secundary">Tareas</RouterLink>
            </li>
          </ul>
        </div>

      </div>
    </nav>
  </header>
</template>