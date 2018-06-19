package com.sidalitechnologies.realestate;

import android.content.Intent;
import android.media.Image;
import android.os.Bundle;
import android.support.design.widget.FloatingActionButton;
import android.support.design.widget.Snackbar;
import android.view.View;
import android.support.design.widget.NavigationView;
import android.support.v4.view.GravityCompat;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBarDrawerToggle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.view.Menu;
import android.view.MenuItem;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import com.sidalitechnologies.Prefrences.PrefManager;

public class Home extends AppCompatActivity
        implements NavigationView.OnNavigationItemSelectedListener {
    ImageView imvchome,imvdist,imvcont,imvmap;
    TextView tvchome,tvdist,tvcont,tvmap;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_home);
        imvchome=(ImageView)findViewById(R.id.imvchome);
        imvdist=(ImageView)findViewById(R.id.imvcdistributer);
        imvcont=(ImageView)findViewById(R.id.imvccontractor);
        imvmap=(ImageView)findViewById(R.id.imcvmap);
        tvchome=(TextView)findViewById(R.id.tvchome);
        tvdist=(TextView)findViewById(R.id.tvcdistributer);
        tvcont=(TextView)findViewById(R.id.tvccontractor);
        tvmap=(TextView)findViewById(R.id.tvcmap);

        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);

//        FloatingActionButton fab = (FloatingActionButton) findViewById(R.id.fab);
//        fab.setOnClickListener(new View.OnClickListener() {
//            @Override
//            public void onClick(View view) {
//                Snackbar.make(view, "Replace with your own action", Snackbar.LENGTH_LONG)
//                        .setAction("Action", null).show();
//            }
//        });

        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(
                this, drawer, toolbar, R.string.navigation_drawer_open, R.string.navigation_drawer_close);
        drawer.addDrawerListener(toggle);
        toggle.syncState();

        NavigationView navigationView = (NavigationView) findViewById(R.id.nav_view);
        navigationView.setNavigationItemSelectedListener(this);
        ImageView imv=(ImageView)toolbar.findViewById(R.id.imvnotification);
        imv.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Toast.makeText(Home.this, "image click", Toast.LENGTH_SHORT).show();
            }
        });
        clicklistnrs();
    }

    @Override
    public void onBackPressed() {
        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        if (drawer.isDrawerOpen(GravityCompat.START)) {
            drawer.closeDrawer(GravityCompat.START);
        } else {
            super.onBackPressed();
        }
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.home, menu);
        getMenuInflater().inflate(R.menu.contracter, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        //noinspection SimplifiableIfStatement
        if (id == R.id.act_logout) {
            Toast.makeText(this, "Logout", Toast.LENGTH_SHORT).show();
            PrefManager prefManager=new PrefManager(this);
            prefManager.clearlogout(this);
            finish();
            return true;
        }

        return super.onOptionsItemSelected(item);
    }

    @SuppressWarnings("StatementWithEmptyBody")
    @Override
    public boolean onNavigationItemSelected(MenuItem item) {
        // Handle navigation view item clicks here.
        int id = item.getItemId();

        if (id == R.id.nav_home) {
            // Handle the camera action
            startActivity(new Intent(Home.this,Home.class));
        } else if (id == R.id.nav_create_home) {
            startActivity(new Intent(Home.this,CreateHome.class));
        } else if (id == R.id.nav_distributer) {
            startActivity(new Intent(Home.this,DistributerView.class));
        } else if (id == R.id.nav_contractor) {
            startActivity(new Intent(Home.this,ContractorView.class));
        }else if (id == R.id.nav_map) {
            startActivity(new Intent(Home.this,PlotView.class));
        }
        else if (id == R.id.nav_msg) {
            startActivity(new Intent(Home.this,Messages.class));
        } else if (id == R.id.nav_logout) {
            PrefManager prefManager=new PrefManager(this);
            prefManager.clearlogout(this);
            finish();
        }

        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        drawer.closeDrawer(GravityCompat.START);
        return true;
    }
    private void clicklistnrs(){
        imvmap.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                startActivity(new Intent(Home.this,PlotView.class));

            }
        });
        imvcont.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                startActivity(new Intent(Home.this,ContractorView.class));
            }
        });
        imvdist.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                startActivity(new Intent(Home.this,DistributerView.class));
            }
        });
        imvchome.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                startActivity(new Intent(Home.this,CreateHome.class));
            }
        });
        tvmap.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                startActivity(new Intent(Home.this,PlotView.class));
            }
        });
        tvcont.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                startActivity(new Intent(Home.this,ContractorView.class));
            }
        });
        tvdist.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                startActivity(new Intent(Home.this,DistributerView.class));
            }
        });
        tvchome.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                startActivity(new Intent(Home.this,CreateHome.class));
            }
        });
    }

}
