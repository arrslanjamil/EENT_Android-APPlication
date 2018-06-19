package com.sidalitechnologies.realestate;

import android.content.Intent;
import android.provider.ContactsContract;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.LinearLayout;

public class CreateHome extends AppCompatActivity {

    LinearLayout liestimation,listructuraldesign,lilabor,libricksandsand;
    LinearLayout listeel,lislab,lisenatorydistri,liplumber;
    LinearLayout licarpenter,lipainter,lialiminumdis,liglassprovider;
    LinearLayout lifurniture;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_create_home);
        liestimation=(LinearLayout)findViewById(R.id.liestimation);
        listructuraldesign=(LinearLayout)findViewById(R.id.listructuraldesign);
        lilabor=(LinearLayout)findViewById(R.id.lilabour);
        libricksandsand=(LinearLayout)findViewById(R.id.librickssand);
        listeel=(LinearLayout)findViewById(R.id.listeelprovider);
        lislab=(LinearLayout)findViewById(R.id.lislabprovider);
        lisenatorydistri=(LinearLayout)findViewById(R.id.lisenatorydis);
        liplumber=(LinearLayout)findViewById(R.id.liplumber);
        lipainter=(LinearLayout)findViewById(R.id.lipainter);
        licarpenter=(LinearLayout)findViewById(R.id.licarpenter);
        lialiminumdis=(LinearLayout)findViewById(R.id.lialuminium);
        liglassprovider=(LinearLayout)findViewById(R.id.liglassprovider);
        lifurniture=(LinearLayout)findViewById(R.id.lifurniture);


        liestimation.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent=new Intent(CreateHome.this,Estimation.class);
                startActivity(intent);
            }
        });
        listructuraldesign.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent=new Intent(CreateHome.this,ViewStructuralDesign.class);

                startActivity(intent);
            }
        });
        lilabor.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent=new Intent(CreateHome.this,DataOnCategories.class);
                intent.putExtra("value","Labour");
                startActivity(intent);
            }
        });
        libricksandsand.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent=new Intent(CreateHome.this,DataOnCategories.class);
                intent.putExtra("value","Bricks and Sand");
                startActivity(intent);
            }
        });
        listeel.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent=new Intent(CreateHome.this,DataOnCategories.class);
                intent.putExtra("value","Steel");
                startActivity(intent);
            }
        });
        lislab.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent=new Intent(CreateHome.this,DataOnCategories.class);
                intent.putExtra("value","Slab");
                startActivity(intent);
            }
        });
        lisenatorydistri.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent=new Intent(CreateHome.this,DataOnCategories.class);
                intent.putExtra("value","Senatory");
                startActivity(intent);
            }
        });
        liplumber.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent=new Intent(CreateHome.this,DataOnCategories.class);
                intent.putExtra("value","Plumber");
                startActivity(intent);
            }
        });
        lipainter.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent=new Intent(CreateHome.this,DataOnCategories.class);
                intent.putExtra("value","Painter");
                startActivity(intent);
            }
        });
        licarpenter.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent=new Intent(CreateHome.this,DataOnCategories.class);
                intent.putExtra("value","Carpenter");
                startActivity(intent);
            }
        });
        lialiminumdis.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent=new Intent(CreateHome.this,DataOnCategories.class);
                intent.putExtra("value","Aluminium");
                startActivity(intent);
            }
        });
        liglassprovider.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent=new Intent(CreateHome.this,DataOnCategories.class);
                intent.putExtra("value","Glass Provider");
                startActivity(intent);
            }
        });
        lifurniture.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent=new Intent(CreateHome.this,DataOnCategories.class);
                intent.putExtra("value","Furniture");
                startActivity(intent);
            }
        });
    }
}
