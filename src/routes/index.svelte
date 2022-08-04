<script lang="ts">
	import Machine from "$lib/machine.svelte";
</script>

<div class="flex-row">
	<div class="flex-column-center">
		<Machine></Machine>
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
