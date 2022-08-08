<script lang="ts">
	import axios from "axios";
	import Machine from "$lib/machine.svelte";

	let token = "";
	let username = "";
	let showMachine = false;

	function login() {
		axios.post<string>("http://localhost:5151/auth/login", { username })
			.then(resp => {
				token = resp.data;	
				showMachine = true;
			});
	}
</script>

<div class="flex-row">
	<div class="flex-column-center">
		{#if showMachine}
		<Machine bind:token={token}></Machine>
		{:else}
			<div>
				<input type="text" placeholder="Name" bind:value={username}>
				<button on:click={login}>Login</button>
			</div>
		{/if}
	</div>
</div>

<style scoped>
.flex-row {
	flex: 1;
	height: 100vh;
	display: flex;
	align-items: center;
	justify-content: center;
}

.flex-column-center {
	flex: 1;
	display: flex;
	justify-content: center;
}
</style>
