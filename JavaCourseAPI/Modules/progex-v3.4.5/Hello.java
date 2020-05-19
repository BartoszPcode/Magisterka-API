public class Hello {
 	public static void main(String[] args){
 		System.out.print("Hello World");		
		int a = 0;
		
		if(a == 0){
			a++;
		}else{
			a--;
		}
		
		for(int i = 0; i<10; i++){
			a= a+2;
			
			if(a == 6){
				a--;
			}
		}
	}
 }